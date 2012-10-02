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
    
    // TODO 
    // TODO 初期状態でレッドブルを指定するとストックを取得できない
    // TODO 投入金額不足 <= もっと上位のサービスかアプリケーション層
    
    @Test
    public void ストックの初期状態ではコーラが5本格納されている() {
        JuiceStock juiceStock = juiceStocks.getAll().get(0);
        assertThat(juiceStock, is(new JuiceStock(cola, 5)));
    }
    
    @Test
    public void ストックにレッドブルを5本追加できる() {
        juiceStocks.add(redBull, 5);
        JuiceStock juiceStock = juiceStocks.getAll().get(1);
        assertThat(juiceStock, is(new JuiceStock(redBull, 5)));
    }
    
    @Test
    public void 投入金額がnullの場合はコーラが購入不可能() {
        JuiceStock juiceStock = juiceStocks.getAll().get(0);
        assertThat(juiceStock.isPurchasable(null), is(false));
    }
    
    @Test
    public void 初期状態で100円投入した場合はコーラが購入不可能() {
        JuiceStock juiceStock = juiceStocks.getAll().get(0);
        assertThat(juiceStock.isPurchasable(100), is(false));
    }
    
    @Test
    public void 初期状態で120円投入した場合はコーラが購入可能() {
        JuiceStock juiceStock = juiceStocks.getAll().get(0);
        assertThat(juiceStock.isPurchasable(120), is(true));
    }
    
    @Test
    public void 初期状態からコーラの出庫を行うと在庫が4つになる() {
        JuiceStock juiceStock = juiceStocks.getAll().get(0);
        juiceStock.delivery();
        assertThat(juiceStock.getCount(), is(4));
    }
    
    @Test
    public void コーラの在庫が0の状態で120円投入した場合は購入不可能() {
        JuiceStock juiceStock = juiceStocks.getAll().get(0);
        for(int i = 0; i < 5; i++) {
            juiceStock.delivery();
        }
        assertThat(juiceStock.isPurchasable(120), is(false));
    }
    
    @Test
    public void 初期状態でコーラを指定してストックを取得できる() {
        JuiceStock juiceStock = juiceStocks.get(cola);
        assertThat(juiceStock.getJuice(), is(cola));
        assertThat(juiceStock.getCount(), is(5));
    }
}
