// Abstract Interface For Hashing Algorithm
// Contain 2 Methods for ComputeSalt and ComputeHash
// Use SignUp Model to Receive Regular Client Password And Manipulate with him 

using MusicPortal.Models.AccountModels;
namespace MusicPortal.Models.Hashing
{
    public interface IHash
    {
        // Compute Hash Method
        public string ComputeHash(string salt, string data);

        // Compute Salt Method
        public string ComputeSalt();
    }
}
