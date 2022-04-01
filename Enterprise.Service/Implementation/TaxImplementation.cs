using System;
using System.Collections.Generic;
using System.Text;

namespace Enterprise.Service.Implementation
{
    public class TaxImplementation : ITaxComputation
    {
        private decimal taxfree;
        private decimal taxbasic;
        private decimal taxhigh;
        private decimal taxaddition;
        private decimal tax;

        public decimal TaxAmount(decimal TaxAmount)
        {
           
            if(TaxAmount <= 1045)
            {
                //tax free for employees earn 1045
                taxfree = .0m;
                tax = TaxAmount * taxfree;
            }
            else if(TaxAmount > 1042 && TaxAmount <= 3125)
            {
                //tax basic for employees earn 1045
                taxbasic = .20m;
                tax = (1042 * .0m) + ((TaxAmount - 1042) * taxbasic);
            }
            else if(TaxAmount > 3125 && TaxAmount <= 12500)
            {
                taxhigh = .40m;
                tax = (1045 * .0m) + ((3125 * 1042)*.20m) + ((TaxAmount - 3125) * taxhigh);

            }
            else if(TaxAmount > 12500)
            {
                taxaddition = .45m;
                tax = (1045 * .0m) + ((3125 * 1045) * .20m) + ((12500 * 3125) * .40m) + ((TaxAmount - 12500) * taxaddition);
            }
            return tax;
        }
    }
}
