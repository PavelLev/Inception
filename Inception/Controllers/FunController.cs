using Microsoft.AspNetCore.Mvc;

namespace Inception.Controllers
{
    public class FunController: Controller
    {
        public string DoNothing(string x, string y)
        {
            return x + y;
        }
    }
}