package net.codersation.vendingmachine.moneyflow;

import java.util.ArrayList;
import java.util.List;

import net.codersation.vendingmachine.Money;

public class MoneyFlow {

	public MoneyPolicy moneyPoricy = new MoneyPolicy();

	private int saleAmount = 0;
	public List<Money> credit = new ArrayList<>();
	public List<Money> change = new ArrayList<>();

	public int getSaleAmount() {
		return saleAmount;
	}

	public void addSale(int price) {
		saleAmount += price;
	}

	public int getTotalAmount() {
		int totalAmount = 0;
		for (Money c : credit) {
			totalAmount += c.getValue();
		}
		return totalAmount;
	}

	public void insert(Money money) {
		if (moneyPoricy.isAllowed(money)) {
			credit.add(money);
		} else {
			change.add(money);
		}
	}

	public void payBack() {
		change.addAll(credit);
		credit.clear();
	}

	public int getChangeAmount() {
		int totalAmount = 0 ;
		for (Money c : change) {
			totalAmount += c.getValue();
		}
		return totalAmount;
	}

}
