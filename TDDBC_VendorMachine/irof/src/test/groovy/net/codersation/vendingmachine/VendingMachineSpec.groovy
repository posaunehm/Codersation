package net.codersation.vendingmachine

import net.codersation.vendingmachine.money.Money
import net.codersation.vendingmachine.stock.Juice
import spock.lang.Specification;
import spock.lang.Unroll;

class VendingMachineSpec extends Specification {

	VendingMachine sut = new VendingMachine()

	def "200円入れてコーラを買って払い戻した。預かり金0円、売上120円、お釣り80円。"() {
		given:
		sut.insert(Money.HundredYen)
		sut.insert(Money.HundredYen)
		when:
		sut.purchase(Juice.create("コーラ"))
		sut.payBack()
		then:
		sut.creditAmount == 0
		sut.saleAmount == 120
		sut.takeOutChange().amount == 80
	}

	def "釣り銭切れると購入しても無反応、売上も増えない"() {
		given:
		sut.insert(Money.ThousandYen)
		sut.purchase(Juice.create("コーラ"))
		sut.payBack()
		sut.insert(Money.ThousandYen)
		when:
		sut.purchase(Juice.create("コーラ"))
		sut.payBack()
		then:
		sut.creditAmount == 0
		sut.saleAmount == 120
		sut.takeOutChange().amount == 1880
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
