package com.codersation.vendmachine.money;

public class ThousandBill extends AmountableMoney {
	protected ThousandBill() {
	}

	@Override
	public Integer getValue() {
		return 1000;
	}

}
