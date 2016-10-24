using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alexon_Service.Models
{
    public class Unit
    {
        private int _id;
        private String _tendonvi;
        private String _madonvi;
        private String _sdt;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public String tendonvi
        {
            get { return _tendonvi; }
            set { _tendonvi = value; }
        }

        public String madonvi
        {
            get { return _madonvi; }
            set { _madonvi = value; }
        }

        public String sdt
        {
            get { return _sdt; }
            set { _sdt = value; }
        }
    }
}
