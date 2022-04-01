using System;
using System.Collections.Generic;
using System.Text;

namespace Enterprise.Service
{
    public interface INationalInsuranceComputation
    {
        decimal NLcAmount(decimal TotalAmount);
    }
}
