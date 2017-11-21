using System;
using System.Security.Cryptography;
using System.Text;
namespace simplechain
{
    public class Block  
    {
        public int Index { get; set; }
        public string Timestamp { get; set; }
        public string Data { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public int Nonce { get; set; }
        public Block(int index,string timestamp,string data,string previousHash = "")
        {
            this.Index = index;
            this.Timestamp = timestamp;
            this.Data = data;
            this.PreviousHash = previousHash;
            this.Nonce = 0;
            this.Hash = CalculateHash();
        }

        public string CalculateHash()
        {
            var sha256 = new SHA256Managed();
            var combinedString = this.Index + this.Timestamp + this.Data + this.PreviousHash + this.Nonce;
            var computedHash = sha256.ComputeHash(Encoding.ASCII.GetBytes(combinedString));
            var hash = new StringBuilder();
            foreach (var b in computedHash)
            {
                hash.Append(b);
            }
            return hash.ToString();
        }

        public void MineBlock(int difficulty)
        {
            while(this.Hash.Substring(0,difficulty) != "".PadLeft(difficulty,'0'))
            {
                this.Nonce++;
                this.Hash = CalculateHash();
            }
        }

    }
}