using JinjiProject.Dtos.Products;
using JinjiProject.Dtos.Subscribers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.VMs.Subscriber
{
    public class ListSubscriberVm
    {
        public ListSubscriberVm()
        {
            SelectedProductIds = new List<int>();
        }
        public List<ListSubscriberDto> ListSubscriberDtos { get; set; }
        public List<ListProductDto> ListProductDtos{ get; set; }
        public IEnumerable<int> SelectedProductIds { get; set; }
    }
}
