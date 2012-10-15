package net.codersation.vendingmachine

import spock.lang.Specification;
import spock.lang.Unroll;

class VendingMachineSpec extends Specification {

	VendingMachine sut = new VendingMachine()

	def "200円入れてコーラを買って払い戻した。売上は120円でお釣りは80円。"() {
		given:
			sut.insert(Money.HundredYen)
			sut.insert(Money.HundredYen)
		when:
			sut.purchase(Juice.Coke)
			sut.payBack()
		then:
			sut.saleAmount == 120
			sut.changeAmount == 80
	}

	@Unroll
	def "Money.#insert を入れると預かり金額が #credit になる"() {
		when:
			sut.insert(insert)
		then:
			sut.creditAmount == credit
		where:
			insert                | credit
			Money.OneYen          | 0
			Money.FiveYen         | 0
			Money.TenYen          | 10
			Money.FiftyYen        | 50
			Money.HundredYen      | 100
			Money.FiveHundredYen  | 500
			Money.ThousandYen     | 1000
			Money.FiveThousandYen | 0
			Money.TwoThousandYen  | 0
			Money.TenThousandYen  | 0
	}
}
