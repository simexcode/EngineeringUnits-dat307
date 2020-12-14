﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UnitConverter {
    class QuantityType : IHashable {
        string Name;
        int HashName;
        string IHashable.Name { get => Name; }
        int IHashable.HashName { get => HashName; }

    }
}
