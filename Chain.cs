using System;
using System.Collections.Generic;

namespace simplechain
{
    public class Chain
    {
        public List<Block> BlockChain { get; set; }
        public int Difficulty { get; set; }

        public Chain(int difficulty)
        {
            this.BlockChain = new List<Block>();
            this.Difficulty = difficulty;
            this.BlockChain.Add(this.CreateGenesisBlock());
        }

        private Block CreateGenesisBlock()
        {
            return new Block(0,"00/00/00","Genesis Block","0");
        }

        public void AddBlock(Block block)
        {
            block.PreviousHash = this.GetLatestBlock().Hash;
            block.MineBlock(this.Difficulty);
            this.BlockChain.Add(block);
        }

        private Block GetLatestBlock()
        {
            return this.BlockChain[this.BlockChain.Count-1];
        }

        public bool isChainValid()
        {
            for (int i = 1; i < this.BlockChain.Count; i++)
            {
                var currentBlock = this.BlockChain[i];
                var previousBlock = this.BlockChain[i-1];

                if(currentBlock.Hash != currentBlock.CalculateHash())
                {
                    return false;
                }
                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
                if(currentBlock.Hash.Substring(0,this.Difficulty) != "".PadLeft(this.Difficulty,'0'))
                {
                    return false;
                }
            }
            return true;
        }
    }
}