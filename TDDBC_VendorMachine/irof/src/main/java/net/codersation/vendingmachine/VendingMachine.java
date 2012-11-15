package net.codersation.vendingmachine;

import net.codersation.vendingmachine.moneyflow.MoneyFlow;
import net.codersation.vendingmachine.moneyflow.MoneyFlowFactory;
import net.codersation.vendingmachine.stockflow.JuiceRack;
import net.codersation.vendingmachine.stockflow.JuiceStock;

/**
 * 自動販売機
 * @author irof
 */
public class VendingMachine {

	private final MoneyFlow moneyFlow;
	private final JuiceStock juiceStock;

	public VendingMachine() {
		moneyFlow = MoneyFlowFactory.create();
		juiceStock = new JuiceStock();
	}

	/**
	 * お金を投入する。
	 * @param money 投入するお金
	 */
	public void insert(Money money) {
		moneyFlow.insert(money);
	}

	/**
	 * 預かったお金を払い戻す。
	 */
	public void payBack() {
		moneyFlow.payBack();
	}

	/**
	 * 預かり金額を取得する。
	 * @return 預かり金額
	 */
	public int getCreditAmount() {
		return moneyFlow.getCreditAmount();
	}

	/**
	 * お釣り金額を取得する。
	 * @return お釣り金額
	 */
	public int getChangeAmount() {
		return moneyFlow.getChangeAmount();
	}

	/**
	 * 売上金額を取得する。
	 * @return 売上金額
	 */
	public int getSaleAmount() {
		return moneyFlow.getSaleAmount();
	}

	/**
	 * ジュースを購入する。
	 * @param juice 購入したいジュース
	 */
	public void purchase(Juice juice) {
		if (!juice.isEnough(getCreditAmount())) {
			return;
		}

		JuiceRack rack = juiceStock.getRack(juice);
		if (rack.isInStock()) {
			rack.remove();
			moneyFlow.purchase(juice.getPrice());
		}
	}

	/**
	 * 購入可能レポートを取得する。
	 * @return 購入可能情報
	 */
	public PurchasableReport getPurchasable() {
		PurchasableReport list = new PurchasableReport();
		for (JuiceRack rack : juiceStock) {
			if (rack.isInStock() && rack.getJuice().isEnough(getCreditAmount())) {
				list.add(rack.getJuice());
			}
		}
		return list;
	}

	/**
	 * 在庫情報レポートを取得する。
	 * @return 在庫情報
	 */
	public StockReport getStockReport() {
		return juiceStock.getStockReport();
	}
}
