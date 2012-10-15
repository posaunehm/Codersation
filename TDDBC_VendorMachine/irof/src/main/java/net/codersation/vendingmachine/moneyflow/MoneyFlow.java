package net.codersation.vendingmachine.moneyflow;

import java.util.List;

import net.codersation.vendingmachine.Money;

public class MoneyFlow {

	public MoneyPolicy moneyPoricy = new MoneyPolicy();

	private int saleAmount = 0;
	private MoneyStock credit = new MoneyStock();
	private MoneyStock change = new MoneyStock();
	private MoneyStock pool = new MoneyStock();

	public MoneyFlow() {
		Money[] moneys = {Money.TenYen, Money.FiftyYen, Money.HundredYen, Money.FiveHundredYen, Money.ThousandYen};
		for (Money money : moneys) {
			for (int i = 0; i < 10; i++) {
				pool.add(money);
			}
		}
	}

	public int getSaleAmount() {
		return saleAmount;
	}

	public void addSale(int price) {
		saleAmount += price;
	}

	public int getCreditAmount() {
		return credit.getAmount();
	}

	public void insert(Money money) {
		if (moneyPoricy.isAllowed(money)) {
			credit.add(money);
		} else {
			change.add(money);
		}
	}

	public void payBack() {
		credit.moveTo(change);
	}

	public int getChangeAmount() {
		return change.getAmount();
	}

	public void purchase(int price) {
		addSale(price);
		
		List<Money> useMoneyList = credit.getUseMoneyList(price);
		int tempAmount = 0;
		for (Money money : useMoneyList) {
			credit.remove(money);
			tempAmount += money.getValue();
		}
		if (tempAmount != price) {
			List<Money> l = pool.getUseMoneyList(tempAmount - price);
			credit.addAll(l);
		}
	}
}
