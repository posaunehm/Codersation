package com.github.springaki.codersation;

public enum Money {
	Ten(10);
	
	private int value;
	
	Money(int value) {
		this.value = value;
	}
	
	public int toInt() {
		return value;
	}

}
