using System;
using System.Collections.Generic;
using System.Text;

namespace UnitConverter {
    interface IHashable {
        public string Name { get; }
        public int HashName { get; }
    }
}
