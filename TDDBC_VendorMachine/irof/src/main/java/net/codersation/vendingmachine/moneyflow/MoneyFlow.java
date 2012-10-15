package net.codersation.vendingmachine.moneyflow;

import net.codersation.vendingmachine.Money;

public class MoneyFlow {

	public MoneyPolicy moneyPoricy = new MoneyPolicy();

	private int saleAmount = 0;
	private final MoneyStock credit = new MoneyStock();
	private final MoneyStock change = new MoneyStock();
	private final MoneyStock pool;

	MoneyFlow(MoneyStock pool) {
		this.pool = pool;
	}

	public int getSaleAmount() {
		return saleAmount;
	}

	private void addSale(int price) {
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
		// creditから払えるだけのお金を取り出す
		MoneyStock sale = credit.takeOut(price);
		// poolからお釣りのために差額のお金を取り出す
		MoneyStock change = pool.takeOut(sale.getAmount() - price);

		sale.moveTo(pool);
		change.moveTo(credit);

		addSale(price);
	}
}
