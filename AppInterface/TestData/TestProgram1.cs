using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProgram1
{
    class TestProgram1
    {
        static void Method(string[] args)
        {

            int deviceConfiguration = (187);   // 10111011
            int carrierMask = 32;            // 00100000
            int connectedMask = 64;          // 01000000

            // Check the carrier
            int carrier = deviceConfiguration & carrierMask;        // Result = 32

            // Check the connection status
            int connected = deviceConfiguration & connectedMask;    // Result = 0

            Console.WriteLine(carrier);
            Console.WriteLine(connected);

            int deviceConfigurationTwo = 187;   // 10111011
            int echoMask = 4;                   // 00000100

            // Set the carrier
            int newConfiguration = deviceConfigurationTwo | echoMask;   // Result = 191

            Console.WriteLine(newConfiguration);
        }
    }
}
