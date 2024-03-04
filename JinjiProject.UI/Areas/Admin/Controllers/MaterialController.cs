using AutoMapper;
using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.BusinessLayer.Validator.CategoryValidations;
using JinjiProject.BusinessLayer.Validator.MaterialValidations;
using JinjiProject.Core.Enums;
using JinjiProject.Dtos.Brands;
using JinjiProject.Dtos.Categories;
using JinjiProject.Dtos.Materials;
using JinjiProject.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace JinjiProject.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MaterialController : BaseController
    {
        private readonly IMaterialService _materialService;
        private readonly IMapper _mapper;

        public MaterialController(IMaterialService materialService, IMapper mapper)
        {
            _materialService = materialService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> MaterialList(bool showWarning = true)
        {
            var materialListResult = await _materialService.GetAllByExpression(material => material.Status == Status.Active || material.Status == Status.Modified);
            var materialList = _mapper.Map<List<ListMaterialDto>>(materialListResult.Data);

            if ((materialList.Count <= 0 || materialList == null) && showWarning)
            {
                NotifyError(materialListResult.Message);
            }
            else if (showWarning)
            {
                NotifySuccess(materialListResult.Message);
            }

            return View(materialList);
        }

        [HttpGet]
        public async Task<IActionResult> CreateMaterial()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMaterial(CreateMaterialDto createMaterialDto)
        {
            CreateMaterialValidator validations = new CreateMaterialValidator();
            var result = validations.Validate(createMaterialDto);

            if (result.IsValid)
            {
                var createMaterialResult = await _materialService.CreateMaterialAsync(createMaterialDto);
                if (createMaterialResult.IsSuccess)
                {
                    NotifySuccess(createMaterialResult.Message);
                }
                else
                {
                    NotifyError(createMaterialResult.Message);

                }
                return RedirectToAction(nameof(MaterialList), new { showWarning = false });
            }

            foreach (var item in result.Errors)
            {
                if (item.ErrorCode == "1")
                {
                    ViewData["NameError"] += item.ErrorMessage + "\n";
                }
                else
                {
                    ViewData["DescriptionError"] += item.ErrorMessage + "\n";

                }
            }

            return View(createMaterialDto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateMaterial(int id)
        {
            var updateMaterialResult = await _materialService.GetMaterialById(id);

            if (updateMaterialResult.IsSuccess)
            {
                UpdateMaterialDto updateMaterial = _mapper.Map<UpdateMaterialDto>(updateMaterialResult.Data);
                return View(updateMaterial);

            }
            else
            {
                UpdateMaterialDto updateMaterial = _mapper.Map<UpdateMaterialDto>(updateMaterialResult.Data);
                NotifyError(updateMaterialResult.Message);
                return RedirectToAction(nameof(MaterialList), new { showWarning = false });
            }

        }


        [HttpPost]
        public async Task<IActionResult> UpdateMaterial(UpdateMaterialDto updateMaterialDto)
        {
            UpdateMaterialValidator updateMaterialValidator = new UpdateMaterialValidator();
            var result = updateMaterialValidator.Validate(updateMaterialDto);

            if (result.IsValid)
            {
                var updateMaterialResult = await _materialService.UpdateMaterialAsync(updateMaterialDto);

                if (updateMaterialResult.IsSuccess)
                {

                    NotifySuccess(updateMaterialResult.Message);

                }
                else
                {
                    NotifyError(updateMaterialResult.Message);
                }

                return RedirectToAction(nameof(MaterialList), new { showWarning = false });
            }

            foreach (var item in result.Errors)
            {
                if (item.ErrorCode == "1")
                {
                    ViewData["NameError"] += item.ErrorMessage + "\n";
                }
                else
                {
                    ViewData["DescriptionError"] += item.ErrorMessage + "\n";

                }
            }
            return View(updateMaterialDto);
        }

        [HttpGet]
        public async Task<IActionResult> SoftDelete(int id)
        {

            var softDeleteResult = await _materialService.SoftDeleteMaterialAsync(id);

            if (softDeleteResult.IsSuccess)
            {
                NotifySuccess(softDeleteResult.Message);


                return RedirectToAction(nameof(MaterialList), new { showWarning = false });
            }
            else
            {
                if (softDeleteResult.Data == null)
                {
                    NotifyError(softDeleteResult.Message);

                    return RedirectToAction(nameof(MaterialList), new { showWarning = false });
                }
                NotifyError(softDeleteResult.Message);

                return RedirectToAction(nameof(MaterialList), new { showWarning = false });
            }
        }


        [HttpGet]
        public async Task<IActionResult> HardDelete(int id)
        {

            var hardDeleteResult = await _materialService.HardDeleteMaterialAsync(id);

            if (hardDeleteResult.IsSuccess)
            {
                NotifySuccess(hardDeleteResult.Message);


                return RedirectToAction(nameof(DeletedMaterialList), new { showWarning = false });
            }
            else
            {
                if (hardDeleteResult.Data == null)
                {
                    NotifyError(hardDeleteResult.Message);

                    return RedirectToAction(nameof(DeletedMaterialList), new { showWarning = false });
                }
                NotifyError(hardDeleteResult.Message);

                return RedirectToAction(nameof(DeletedMaterialList), new { showWarning = false });
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeletedMaterialList(bool showWarning = true)
        {

            var deletedMaterial = await _materialService.GetAllByExpression(x => x.Status == Status.Deleted);
            List<DeletedMaterialListDto> deletedMaterialList = _mapper.Map<List<DeletedMaterialListDto>>(deletedMaterial.Data);

            if ((deletedMaterialList.Count <= 0 || deletedMaterialList == null) && showWarning)
            {
                NotifyError("Silinen Malzeme Listesi Boş");
            }
            else if (showWarning)
            {
                NotifySuccess("Silinen Malzemeler Listelendi");

            }

            return View(deletedMaterialList);

        }


        [HttpGet]
        public async Task<IActionResult> AddAgainMaterial(int id)
        {
            var materialToAdded = await _materialService.GetMaterialById(id);

            if (materialToAdded.Data == null)
            {
                NotifyError(materialToAdded.Message);
                return RedirectToAction(nameof(DeletedMaterialList), new { showWarning = false });
            }
            else
            {

                materialToAdded.Data.Status = Status.Active;
                UpdateMaterialDto updatedToMaterial = _mapper.Map<UpdateMaterialDto>(materialToAdded.Data);

                var materialToUpdated = await _materialService.UpdateMaterialAsync(updatedToMaterial);
                NotifySuccess("Malzeme yeniden eklendi.");

                return RedirectToAction(nameof(DeletedMaterialList), new { showWarning = false });
            }
        }

        [HttpGet]
        public async Task<DetailMaterialDto> GetDetailMaterial(int materialid)
        {

            var material = await _materialService.GetMaterialById(materialid);
            var materialResult = _mapper.Map<DetailMaterialDto>(material.Data);

            return materialResult;
        }

        [HttpGet]
        public async Task<GetMaterialDto> GetMaterial(int materialid)
        {

            var materialResult = await _materialService.GetMaterialById(materialid);

            return materialResult.Data;
        }
    }
}
