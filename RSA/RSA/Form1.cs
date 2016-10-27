using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace RSA
{
    public partial class Form1 : Form
    {
        // Strings to hold the keys
        private String publicKey, privateKey;

        // Encoder
        UnicodeEncoding encoder = new UnicodeEncoding();

        public Form1()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            InitializeComponent();
            privateKey = rsa.ToXmlString(true);
            publicKey = rsa.ToXmlString(false);
        }

        private void btnClearPlain_Click(object sender, EventArgs e)
        {
            txtPlainText.Clear();
        }

        private void btnClearEncrypted_Click(object sender, EventArgs e)
        {
            txtCipherText.Clear();
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            // Set up crypto service provider
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            StringBuilder stringBuilder = new StringBuilder();

            // Split each data segment into an array
            var dataArray = txtCipherText.Text.Split(new char[] { ',' });
            foreach (var data in dataArray)
            {
                // Split the bytes of this segment into an array
                var dataPart = data.Trim().Split(new char[] { ' ' });
                byte[] dataByte = new byte[dataPart.Length];

                // Convert from string to bytes
                for (int i = 0; i < dataByte.Length; i++)
                {
                    dataByte[i] = Convert.ToByte(dataPart[i]);
                }

                // Decrypt the byte array
                var decryptedBytes = rsa.Decrypt(dataByte, false);
                stringBuilder.Append(encoder.GetString(decryptedBytes));
            }
            txtPlainText.Text = stringBuilder.ToString();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            // Set up crypto service provider
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey); // Use public key for encryption
            var stringBuilder = new StringBuilder();

            // Encode data
            var dataToEncrypt = encoder.GetBytes(txtPlainText.Text);
            var startIndex = 0;
            while (startIndex <= dataToEncrypt.Length) {
                var data = new byte[96];
                for (int i = 0; i < Math.Min(dataToEncrypt.Length - startIndex, 96); i++)
                {
                    data[i] = dataToEncrypt[startIndex + i];
                }

                // Encrypt byte array
                var encryptedByteArray = rsa.Encrypt(data, false).ToArray();

                // Change each byte in the encrypted byte array to text
                foreach (var x in encryptedByteArray)
                {
                    stringBuilder.Append(x);
                    stringBuilder.Append(" ");
                }
                startIndex += 96;
                stringBuilder.Append(",");
            }
            // Remove the last comma
            txtCipherText.Text = stringBuilder.ToString().Substring(0, stringBuilder.Length - 1);
        }
    }
}
