package net.codersation.vendingmachine;

public class JuiceFactory {

	public static Juice create(String name) {
		switch (name) {
		case "コーラ":
			return new Juice("コーラ", 120);
		case "水":
			return new Juice("水", 100);
		case "レッドブル":
			return new Juice("レッドブル", 200);
		default:
			throw new IllegalArgumentException(name);
		}
	}
}
