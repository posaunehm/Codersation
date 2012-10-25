package net.codersation.vendingmachine.stockflow;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import net.codersation.vendingmachine.Juice;
import net.codersation.vendingmachine.StockReport;

public class JuiceStock implements Iterable<JuiceRack> {

	private List<JuiceRack> racks = new ArrayList<JuiceRack>();

	public JuiceStock() {
		initialize();
	}

	@Override
	public Iterator<JuiceRack> iterator() {
		return racks.iterator();
	}

	private void initialize() {
		for (Juice juice : Juice.values()) {
			racks.add(new JuiceRack(juice, 5));
		}
	}

	public JuiceRack getRack(Juice juice) {
		for (JuiceRack rack : racks) {
			if (rack.getJuice().equals(juice)) {
				return rack;
			}
		}
		throw new IllegalStateException("そんなRackはない");
	}

	public boolean isInStock(Juice juice) {
		return getRack(juice).isInStock();
	}

	public StockReport getStockReport() {
		StockReport report = new StockReport();
		for (JuiceRack rack : racks) {
			report.put(rack.getJuice(), rack.getCount());
		}
		return report;
	}
}
