package net.codersation.vendingmachine;

import static org.hamcrest.CoreMatchers.is;
import static org.junit.Assert.assertThat;

import java.io.BufferedReader;
import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.PrintStream;
import java.io.StringReader;

import org.junit.Rule;
import org.junit.Test;
import org.junit.rules.TestWatcher;

public class MainTest {

	@Rule
	public SysOutReader reader = new SysOutReader();

	@Test
	public void お金の投入はinsでする() throws Exception {
		byte[] input = "ins 100\nins 10\n".getBytes();
		System.setIn(new ByteArrayInputStream(input));

		Main.main();

		assertThat(reader.readLine(), is("> "));
		assertThat(reader.readLine(), is("money: 100 was received."));
		assertThat(reader.readLine(), is("> "));
		assertThat(reader.readLine(), is("money: 10 was received."));
	}

	@Test
	public void 変なお金をinsしたらば() throws Exception {
		byte[] input = "ins 123".getBytes();
		System.setIn(new ByteArrayInputStream(input));

		Main.main();

		assertThat(reader.readLine(), is("> "));
		assertThat(reader.readLine(), is("! 123 is not available."));
	}

	static class SysOutReader extends TestWatcher {
		ByteArrayOutputStream baos;
		BufferedReader reader;

		protected void starting(org.junit.runner.Description description) {
			baos = new ByteArrayOutputStream();
			System.setOut(new PrintStream(baos));
		};

		public String readLine() throws IOException {
			if (reader == null) {
				reader = new BufferedReader(new StringReader(baos.toString("UTF-8")));
			}
			return reader.readLine();
		}
	};
}
