using System;
using RotMG.Common.IO;
using RotMG.Common.Networking.IO;

namespace RotMG.Common.Networking.Data
{
    // Token: 0x02000051 RID: 81
    [DataType]
    public struct ConditionEffect
    {
        // Token: 0x0600027A RID: 634 RVA: 0x0000903A File Offset: 0x0000723A
        public ConditionEffect(uint condEff)
        {
            this.Effect = condEff;
        }

        // Token: 0x0600027B RID: 635 RVA: 0x00009043 File Offset: 0x00007243
        private static uint NumberOfSetBits(uint i)
        {
            i -= (i >> 1 & 1431655765u);
            i = (i & 858993459u) + (i >> 2 & 858993459u);
            return (i + (i >> 4) & 252645135u) * 16843009u >> 24;
        }

        // Token: 0x0600027C RID: 636 RVA: 0x0000907C File Offset: 0x0000727C
        public static ConditionEffect Load(ref ByteBuffer buffer)
        {
            uint num = 0u;
            for (byte b = buffer.ReadByte(); b > 0; b -= 1)
            {
                num |= 1u << (int)buffer.ReadByte();
            }
            return new ConditionEffect(num);
        }

        // Token: 0x0600027D RID: 637 RVA: 0x000090B4 File Offset: 0x000072B4
        public static void Save(ref ByteBuffer buffer, ConditionEffect condEff)
        {
            if (condEff.Effect == 0u)
            {
                buffer.WriteByte(0);
                return;
            }
            buffer.WriteByte((byte)ConditionEffect.NumberOfSetBits(condEff.Effect));
            uint num = condEff.Effect;
            byte b = 0;
            while (num != 0u)
            {
                if ((num & 1u) != 0u)
                {
                    buffer.WriteByte(b);
                }
                b += 1;
                num >>= 1;
            }
        }

        // Token: 0x0600027E RID: 638 RVA: 0x00009109 File Offset: 0x00007309
        public static int SizeOf(ConditionEffect condEff)
        {
            if (condEff.Effect == 0u)
            {
                return 1;
            }
            return (int)(1u + ConditionEffect.NumberOfSetBits(condEff.Effect));
        }

        // Token: 0x040001D9 RID: 473
        public uint Effect;
    }
}
