using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Core
{
    /// <summary>
    /// Emerald Core
    /// 
    /// Result core class
    /// 
    /// Handles user input validation. 
    /// 
    /// Created: 2020-02-28
    /// 
    /// <list type="bullet">
    ///     T: The class you wish to implement
    ///     
    /// </list>
    /// </summary>
    public abstract class Result<T> : EmeraldComponent where T : ValidationClass
    {
        public new static int API_VERSION_MAJOR = 0;
        public new static int API_VERSION_MINOR = 1;
        public new static int API_VERSION_REVISION = 0;
        public override bool Experimental { get => base.Experimental; set => base.Experimental = value; }

        public Result()
        {
            
        }

        public abstract T Validate(object ObjectValidate);

    }
}
