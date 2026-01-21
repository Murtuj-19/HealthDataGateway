using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HealthDataGateway.Services.Interfaces
{
    public interface IEncryptionService
    {
        byte[] Encrypt(string plainText);
        string Decrypt(byte[] cipherText);
    }
}