package codersation.vendingmachine;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class CreditService {

	public static List<Money> getUseMoneyList(List<Money> list, int i) {

		List<Money> result = new ArrayList<>();

		Collections.sort(list);
		for (Money money : list) {
			if (i <= 0) {
				break;
			}
			if (money.getValue() < i) {
				result.add(money);
				i -= money.getValue();
			} else {
				result.add(money);
				i -= money.getValue();
			}
		}

		for (Money money : result.toArray(new Money[0])) {
			if (i <= money.getValue() * -1) {
				result.remove(money);
				i += money.getValue();
			}
		}
		return result;
	}
}
