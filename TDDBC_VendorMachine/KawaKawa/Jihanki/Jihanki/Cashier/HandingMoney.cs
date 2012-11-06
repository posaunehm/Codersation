using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jihanki.Cashier
{
    /// <summary>
    /// 自販機が取り扱う金額種別を判定
    /// </summary>
    public class HandingMoney:IDisposable
    {
        /// <summary>
        /// 取り扱いする金額種別リスト
        /// </summary>
        private List<Base.MoneyKind.Kind> handlingMoneyKindList = new List<Base.MoneyKind.Kind>();



        public HandingMoney()
        {
            this.SetHandlingMoneyKindList();
        }


        /// <summary>
        /// 取り扱う金額種別を指定
        /// </summary>
        private void SetHandlingMoneyKindList()
        {

            this.handlingMoneyKindList.Clear();
            this.handlingMoneyKindList.Add(Base.MoneyKind.Kind.Yen10);
            this.handlingMoneyKindList.Add(Base.MoneyKind.Kind.Yen50);
            this.handlingMoneyKindList.Add(Base.MoneyKind.Kind.Yen100);
            this.handlingMoneyKindList.Add(Base.MoneyKind.Kind.Yen500);
            this.handlingMoneyKindList.Add(Base.MoneyKind.Kind.Yen1000);
            
        }
        



        /// <summary>
        /// 取り扱いしているお金かチェック
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public bool IsHandling(Base.Money money)
        {
            //指定されたお金が取り扱い金額種別かチェック
            var query = this.handlingMoneyKindList.Contains(money.GetKind());


            return query;
        }







        public void Dispose()
        {
            this.handlingMoneyKindList.Clear();
            this.handlingMoneyKindList = null;
        }
    }
}
