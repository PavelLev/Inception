using Microsoft.AspNetCore.Mvc;

namespace Inception.Controllers
{
    public class FunController: Controller
    {
        public string DoNothing(string x, string y, FunClass funClass)
        {
            return x + y + funClass.z;
        }

        public class FunClass
        {
            public string z;
        }
    }
}