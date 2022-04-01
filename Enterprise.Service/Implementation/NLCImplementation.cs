using System;
using System.Collections.Generic;
using System.Text;

namespace Enterprise.Service.Implementation
{
    public class NLCImplementation : INationalInsuranceComputation
    {
        private decimal NICRate;
        private decimal NIC;

        // below 719 0% Rate 
        //Above 719 - 4167 12% Rate
        // Above 4167 2% Rate
        public decimal NLcAmount(decimal TotalAmount)
        {
            if(TotalAmount < 719)
            {
                NICRate = .0m;
                NIC = TotalAmount * NICRate;
            }
            else if(TotalAmount >= 719 && TotalAmount <= 4167)
            {
                NICRate = .12m;
                NIC = (719 * .0m) + ((TotalAmount - 719) * NICRate);
            }
            else if(TotalAmount > 4167)
            {
                NICRate = .02m;
                NIC = ((4167 - 719) * .12m) + ((TotalAmount - 4167) * NICRate);

            }
            return NIC;
            
        }
    }
}
