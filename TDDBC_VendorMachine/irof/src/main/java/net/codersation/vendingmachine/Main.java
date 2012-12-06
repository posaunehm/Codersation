package net.codersation.vendingmachine;

import java.util.Scanner;

public class Main {

	public static void main(String... args) {
		VendingMachine vendingMachine = new VendingMachine();

		System.out.println("Insert?> ");
		try (Scanner scanner = new Scanner(System.in)) {
			while (scanner.hasNext()) {
				Money money = null;
				switch (scanner.nextLine()) {
				case "100":
					money = Money.HundredYen;
					break;
				case "10":
					money = Money.TenYen;
					break;
				}

				vendingMachine.insert(money);
				System.out.println("credit: " + vendingMachine.getCreditAmount());
				System.out.println("Insert?> ");
			}
		}
	}
}
