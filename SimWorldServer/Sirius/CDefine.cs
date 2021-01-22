using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirius
{
    public class CDefine
    {
        /// <summary>
        /// 资源类型
        /// </summary>
        public enum eResType
        {
            Null = 0,
            Iron,
            Copper,
            Gold,
            Wood,
            Clay,
            LimeStone,
            IronR,
            CopperR,
            Coin, //等同于Money
        }

    }
}
