package com.codersation.vendmachine.money;

public class TenYenCoin extends AmountableMoney {
	protected TenYenCoin() {
	}
	
	@Override
	public Integer getValue() {
		return 10;
	}
}
