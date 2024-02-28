using AutoMapper;
using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.Core.Enums;
using JinjiProject.Dtos.Brands;
using JinjiProject.Dtos.Materials;
using Microsoft.AspNetCore.Mvc;

namespace JinjiProject.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MaterialController : Controller
    {
        private readonly IMaterialService _materialService;
        private readonly IMapper _mapper;

        public MaterialController(IMaterialService materialService, IMapper mapper)
        {
            _materialService = materialService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> MaterialList()
        {
            var materialListResult = await _materialService.GetAllByExpression(material => material.Status == Status.Active || material.Status == Status.Modified);
            var materialList = _mapper.Map<List<ListMaterialDto>>(materialListResult.Data);
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
            var createResult = await _materialService.CreateMaterialAsync(createMaterialDto); ;

            return RedirectToAction(nameof(MaterialList));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateMaterial(int id)
        {
            var updateMaterialResult = await _materialService.GetMaterialById(id);
            UpdateMaterialDto updateMaterial = _mapper.Map<UpdateMaterialDto>(updateMaterialResult.Data);
            return View(updateMaterial);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateMaterial(UpdateMaterialDto updateMaterialDto)
        {
            var updateMaterial = await _materialService.UpdateMaterialAsync(updateMaterialDto);

            return RedirectToAction(nameof(MaterialList));
        }

        [HttpGet]
        public async Task<IActionResult> SoftDelete(int id)
        {

            await _materialService.SoftDeleteMaterialAsync(id);

            return RedirectToAction(nameof(MaterialList));
        }


        [HttpGet]
        public async Task<IActionResult> HardDelete(int id)
        {

            await _materialService.SoftDeleteMaterialAsync(id);

            return RedirectToAction(nameof(MaterialList));
        }

        [HttpGet]
        public async Task<IActionResult> DeletedMaterialList()
        {

            var deletedMaterial = await _materialService.GetAllByExpression(x => x.Status == Status.Deleted);
            List<DeletedMaterialListDto> deletedMaterialList = _mapper.Map<List<DeletedMaterialListDto>>(deletedMaterial.Data);
            return View(deletedMaterialList);

        }


        [HttpGet]
        public async Task<IActionResult> AddAgainMaterial(int id)
        {
            var materialToAdded = await _materialService.GetMaterialById(id);

            if (materialToAdded.Data == null)
            {
                return View();
            }
            else
            {

                materialToAdded.Data.Status = Status.Active;
                UpdateMaterialDto updatedToMaterial = _mapper.Map<UpdateMaterialDto>(materialToAdded.Data);
                await _materialService.UpdateMaterialAsync(updatedToMaterial);
                return RedirectToAction("MaterialList", "Material");
            }
        }
    }
}
