package codersation.vendingmachine;

import java.util.ArrayList;
import java.util.EnumMap;
import java.util.List;
import java.util.Map;

public class VendingMachine {

	private int totalAmount = 0;
	private int change = 0;
	private MoneyPolicy moneyPoricy = new MoneyPolicy();
	private Map<Juice, Integer> juiceStock = new EnumMap<>(Juice.class);
	private int saleAmount = 0;

	public VendingMachine() {
		for (Juice juice : Juice.values()) {
			juiceStock.put(juice, juice.getInitialStock());
		}
	}

	public int getTotalAmount() {
		return totalAmount;
	}

	public void insert(Money money) {
		if (moneyPoricy.isAllowed(money)) {
			totalAmount += money.getValue();
		} else {
			change += money.getValue();
		}
	}

	public void payBack() {
		change += totalAmount;
		totalAmount = 0;
	}

	public int getChangeAmount() {
		return change;
	}

	int getStockCount(Juice juice) {
		return juiceStock.get(juice);
	}

	boolean canPurchase(Juice juice) {
		if (getStockCount(juice) <= 0) {
			return false;
		}
		return totalAmount >= juice.getPrice();
	}

	public void purchase(Juice juice) {
		if (canPurchase(juice)) {
			juiceStock.put(juice, juiceStock.get(juice) - 1);
			saleAmount += juice.getPrice();
			totalAmount -= juice.getPrice();
		}
	}

	public int getSaleAmount() {
		return saleAmount;
	}

	public List<Juice> getPurchasable() {
		List<Juice> list = new ArrayList<>();
		for (Juice juice : Juice.values()) {
			if (canPurchase(juice)) {
				list.add(juice);
			}
		}
		return list;
	}
}
