package net.codersation.vendingmachine.money;

public class MoneyFlowFactory {

    public static MoneyFlow create() {
        MoneyStock initialStock = new MoneyStock();

        // TODO 初期状態でお釣りとして持っているもの……だけどもう少しどうにかならんのかと
        Money[] moneys = {Money.TenYen, Money.FiftyYen, Money.HundredYen, Money.FiveHundredYen, Money.ThousandYen};
        for (Money money : moneys) {
            for (int i = 0; i < 10; i++) {
                initialStock.add(money);
            }
        }
        return new MoneyFlow(initialStock);
    }

}
