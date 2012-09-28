package codersation.vendingmachine;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class VendingMachine {

	private List<Money> credit = new ArrayList<>();
	
	private List<Money> change = new ArrayList<>();
	private MoneyPolicy moneyPoricy = new MoneyPolicy();
	private List<JuiceStock> juiceStock = new ArrayList<>();
	private int saleAmount = 0;

	public VendingMachine() {
		for (Juice juice : Juice.values()) {
			juiceStock.add(new JuiceStock(juice, 5));
		}
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

	public void purchase(Juice juice) {
		JuiceStock stock = getJuiceStock(juice);
		if (stock.canPurchase(getTotalAmount())) {
			stock.remove();

			saleAmount += juice.getPrice();
			
			List<Money> useMoneyList = CreditService.getUseMoneyList(credit, juice.getPrice());
			int tempAmount = 0;
			for (Money money : useMoneyList) {
				credit.remove(money);
				tempAmount += money.getValue();
			}
			if (tempAmount != juice.getPrice()) {
				List<Money> list = new ArrayList<>(Collections.nCopies(100, Money.TenYen));
				List<Money> l = CreditService.getUseMoneyList(list, tempAmount - juice.getPrice());
				credit.addAll(l);
			}
		}
	}

	private JuiceStock getJuiceStock(Juice juice) {
		for (JuiceStock stock : juiceStock) {
			if (stock.getJuice().equals(juice)) {
				return stock;
			}
		}
		throw new IllegalStateException("そんなStockはない");
	}

	public int getSaleAmount() {
		return saleAmount;
	}

	public List<Juice> getPurchasable() {
		List<Juice> list = new ArrayList<>();
		for (Juice juice : Juice.values()) {
			if (getJuiceStock(juice).canPurchase(getTotalAmount())) {
				list.add(juice);
			}
		}
		return list;
	}

	public List<JuiceStock> getAllJuiceStock() {
		return juiceStock;
	}
}
