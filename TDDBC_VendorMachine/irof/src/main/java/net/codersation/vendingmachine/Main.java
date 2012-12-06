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
						boolean flg = false;
						for (Money money : Money.values()) {
							if (money.getValue() == amount) {
								flg = true;
								vendingMachine.insert(money);
								System.out.printf("money: %d was received.%n", amount);
								break;
							}
						}
						if (!flg) {
							System.out.printf("! %s is not available.%n", amountString);
						}
					}
				}
				System.out.println("> ");
			}
		}
	}
}
