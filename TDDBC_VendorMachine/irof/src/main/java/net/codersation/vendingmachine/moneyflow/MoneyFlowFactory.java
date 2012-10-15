package net.codersation.vendingmachine.moneyflow;

import net.codersation.vendingmachine.Money;

public class MoneyFlowFactory {

	public static MoneyFlow create() {
		MoneyStock pool = new MoneyStock();
		Money[] moneys = {Money.TenYen, Money.FiftyYen, Money.HundredYen, Money.FiveHundredYen, Money.ThousandYen};
		for (Money money : moneys) {
			for (int i = 0; i < 10; i++) {
				pool.add(money);
			}
		}
		MoneyFlow moneyFlow = new MoneyFlow(pool);
		return moneyFlow;
	}

}
