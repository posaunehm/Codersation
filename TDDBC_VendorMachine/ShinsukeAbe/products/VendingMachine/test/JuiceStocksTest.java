/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;
import static org.hamcrest.core.Is.*;

/**
 *
 * @author mao
 */
public class JuiceStocksTest {
    
    public JuiceStocksTest() {
    }
    
    @BeforeClass
    public static void setUpClass() {
    }
    
    @AfterClass
    public static void tearDownClass() {
    }
    
    @Before
    public void setUp() {
    }
    
    @After
    public void tearDown() {
    }
    
    // TODO ストックにレッドブルを5本追加できる
    @Test
    public void ストックの初期状態はコーラが格納されている() {
        JuiceStocks juiceStocks = JuiceStocksFactory.createNewStocks();
        JuiceStock juiceStock = juiceStocks.getAllStocks().get(0);
        Juice juice = juiceStock.getJuice();
        assertThat(juice.getName(), is("コーラ"));
        assertThat(juice.getPrice(), is(120));
    }
    
    @Test
    public void ストックの初期状態では5本格納されている() {
        JuiceStocks juiceStocks = JuiceStocksFactory.createNewStocks();
        JuiceStock juiceStock = juiceStocks.getAllStocks().get(0);
        assertThat(juiceStock.getCount(), is(5));
    }
    
    @Test
    public void ストックにレッドブルを5本追加できる() {
        JuiceStocks juiceStocks = JuiceStocksFactory.createNewStocks();
        juiceStocks.addStock(new Juice("レッドブル", 200), 5);
        JuiceStock juiceStock = juiceStocks.getAllStocks().get(1);
        Juice juice = juiceStock.getJuice();
        assertThat(juice.getName(), is("レッドブル"));
        assertThat(juice.getPrice(), is(200));
        assertThat(juiceStock.getCount(), is(5));
    }
}
