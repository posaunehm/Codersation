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
    
    private JuiceStocks juiceStocks;
    
    private Juice cola = new Juice("コーラ", 120);
    private Juice redBull = new Juice("レッドブル", 200);
    
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
        juiceStocks = JuiceStocksFactory.createNewStocks();
    }
    
    @After
    public void tearDown() {
    }
    
    // TODO 初期状態で100円投入した場合はコーラが購入不可能
    // TODO 初期状態で120円投入した場合はコーラが購入可能
    // TODO 初期状態で200円投入した場合はコーラが購入可能
    // TODO 初期状態からコーラの出庫を行うと在庫が4本になる
    // TODO コーラの在庫が0の状態で120円投入した場合は購入不可能
    // TODO 投入金額不足 <= もっと上位のサービスかアプリケーション層
    
    @Test
    public void ストックの初期状態ではコーラが5本格納されている() {
        JuiceStock juiceStock = juiceStocks.getAllStocks().get(0);
        assertThat(juiceStock, is(new JuiceStock(cola, 5)));
    }
    
    @Test
    public void ストックにレッドブルを5本追加できる() {
        juiceStocks.addStock(redBull, 5);
        JuiceStock juiceStock = juiceStocks.getAllStocks().get(1);
        assertThat(juiceStock, is(new JuiceStock(redBull, 5)));
    }
    
    @Test
    public void 投入金額がnullの場合はコーラが購入不可能() {
        JuiceStock juiceStock = juiceStocks.getAllStocks().get(0);
        assertThat(juiceStock.isPurchasable(null), is(false));
    }
    
    @Test
    public void 初期状態で100円投入した場合はコーラが購入不可能() {
        JuiceStock juiceStock = juiceStocks.getAllStocks().get(0);
        assertThat(juiceStock.isPurchasable(100), is(false));
    }
}
