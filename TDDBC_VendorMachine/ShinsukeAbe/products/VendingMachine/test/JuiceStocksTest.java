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
import static org.hamcrest.core.IsNull.*;

/**
 *
 * @author mao
 */
public class JuiceStocksTest {
    
    private JuiceStocks juiceStocks;
    private JuiceStock defaultStock;
    
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
        defaultStock = juiceStocks.getAll().get(0);
    }
    
    @After
    public void tearDown() {
    }
    
    // TODO 投入金額不足 <= もっと上位のサービスかアプリケーション層
    
    @Test
    public void ストックの初期状態ではコーラが5本格納されている() {
        assertThat(defaultStock, is(new JuiceStock(cola, 5)));
    }
    
    @Test
    public void ストックにレッドブルを5本追加できる() {
        juiceStocks.add(redBull, 5);
        JuiceStock juiceStock = juiceStocks.getAll().get(1);
        assertThat(juiceStock, is(new JuiceStock(redBull, 5)));
    }
    
    @Test
    public void 投入金額がnullの場合はコーラが購入不可能() {
        assertThat(defaultStock.isPurchasable(null), is(false));
    }
    
    @Test
    public void 初期状態で100円投入した場合はコーラが購入不可能() {
        assertThat(defaultStock.isPurchasable(100), is(false));
    }
    
    @Test
    public void 初期状態で120円投入した場合はコーラが購入可能() {
        assertThat(defaultStock.isPurchasable(120), is(true));
    }
    
    @Test
    public void 初期状態からコーラの出庫を行うと在庫が4つになる() {
        defaultStock.delivery();
        assertThat(defaultStock.getCount(), is(4));
    }
    
    @Test
    public void コーラの在庫が0の状態で120円投入した場合は購入不可能() {
        for(int i = 0; i < 5; i++) {
            defaultStock.delivery();
        }
        assertThat(defaultStock.isPurchasable(120), is(false));
    }
    
    @Test
    public void 初期状態でコーラを指定してストックを取得できる() {
        JuiceStock juiceStock = juiceStocks.get(cola);
        assertThat(juiceStock.getJuice(), is(cola));
        assertThat(juiceStock.getCount(), is(5));
    }
    
    @Test
    public void 初期状態でレッドブルを指定するとストックを取得できない() {
        JuiceStock juiceStock = juiceStocks.get(redBull);
        assertThat(juiceStock, nullValue());
    }
}
