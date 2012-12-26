package net.codersation.vendingmachine.money;

import static net.codersation.vendingmachine.money.Money.*;

public class MoneyFlowFactory {

    public static MoneyFlow create() {
        MoneyStock initialStock = new MoneyStock();
        initialStock.add(TenYen, 10);
        initialStock.add(FiftyYen, 10);
        initialStock.add(HundredYen, 10);
        initialStock.add(FiveHundredYen, 10);
        initialStock.add(ThousandYen, 10);

        return new MoneyFlow(initialStock);
    }

}
