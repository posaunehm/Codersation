package net.codersation.vendingmachine;

import java.util.Scanner;

public class Main {

	public static void main(String... args) {
		VendingMachine vendingMachine = new VendingMachine();
		System.out.println("> ");
		try (Scanner scanner = new Scanner(System.in)) {
			while (scanner.hasNext()) {
				String input = scanner.nextLine();
				if (input.startsWith("ins")) {
					String[] split = input.split(" +");
					if (split.length > 1) {
						String amountString = split[1];
						int amount = Integer.parseInt(amountString);
						for (Money money : Money.values()) {
							if (money.getValue() == amount) {
								vendingMachine.insert(money);
								System.out.printf("money: %d was received.%n", amount);
								break;
							}
						}
					}
				}
				System.out.println("> ");
			}
		}
	}
}
