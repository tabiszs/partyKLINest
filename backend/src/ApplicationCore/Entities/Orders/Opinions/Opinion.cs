using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyKlinest.ApplicationCore.Entities.Orders.Opinions
{
    public class Opinion
    {
        public long OpinionId { get; set; }
        public int Rating { get; set; }
        public string AdditionalInfo { get; set; }
        public string? BeforePhotoUri { get; set; }
        public string? AfterPhotoUri { get; set; }
    }
}
