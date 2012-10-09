package net.codersation.vendingmachine;

public enum Money {
	OneYen(1),
	FiveYen(5),
	TenYen(10),
	FiftyYen(50),
	HundredYen(100),
	FiveHundredYen(500),
	ThousandYen(1000),
	TwoThousandYen(2000),
	FiveThousandYen(5000),
	TenThousandYen(10000);

	private final int value;

	Money(int value) {
		this.value = value;
	}

	public int getValue() {
		return value;
	}
}
