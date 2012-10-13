package net.codersation.vendingmachine;

import java.util.List;

import net.codersation.vendingmachine.moneyflow.MoneyStock;

public class CreditService {

	/**
	 * @deprecated Use {@link MoneyStock#getUseMoneyList(List<Money>,int)} instead
	 */
	public static List<Money> getUseMoneyList(List<Money> list, int i) {
		MoneyStock stock = new MoneyStock();
		stock.addAll(list);
		return stock.getUseMoneyList(i);
	}
}
