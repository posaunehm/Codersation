package com.codersation.vendmachine.money;

public class AmountableMoneyFactory {
	private static final int TEN = 10;
	private static final int FIFTY = 50;
	private static final int HUNDRED = 100;
	private static final int FIVEHUNDRED = 500;
	private static final int THOUSAND = 1000;
	
	public static AmountableMoney createNewMoney(Integer value) throws Exception {
		switch(value) {
		case TEN:
			return new TenYenCoin();
		case FIFTY:
			return new FiftyYenCoin();
		case HUNDRED:
			return new HundredYenCoin();
		case FIVEHUNDRED:
			return new FiveHundredCoin();
		case THOUSAND:
			return new ThousandBill();
		default:
			throw new Exception("Un amountable money." + value.toString());
		}
	}
}
