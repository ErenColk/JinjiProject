using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Helpers
{
    public class ImageHelper
    {
      public static byte[] ReadImageBytesFromFile(string imagePath)
{
    return File.ReadAllBytes(imagePath);
}

    }
}
