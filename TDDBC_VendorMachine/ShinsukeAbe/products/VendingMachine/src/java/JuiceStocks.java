
import java.util.ArrayList;
import java.util.List;

/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author mao
 */
public class JuiceStocks {
    
    private List<JuiceStock> stocks = new ArrayList<JuiceStock>();

    List<JuiceStock> getAll() {
        return stocks;
    }

    void add(Juice juice, int count) {
        stocks.add(new JuiceStock(juice, count));
    }
    
}
