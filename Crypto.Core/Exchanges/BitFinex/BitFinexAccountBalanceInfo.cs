﻿using Crypto.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.BitFinex {
    public class BitFinexAccountBalanceInfo : BalanceBase {
        public BitFinexAccountBalanceInfo(AccountInfo info) : base(info) { }
    }
}
