<script lang="ts">
  import Nested from "./Nested.svelte";
  import { time } from "./stores";

  let count: number = 0;
  const increment = () => {
    count += 1;
  };

  const formatter = new Intl.DateTimeFormat("en", {
    hour12: true,
    hour: "numeric",
    minute: "2-digit",
    second: "2-digit",
  });
</script>

<button on:click={increment}>
  count is {count}
</button>
<Nested />

<h1>The time is {formatter.format($time)}</h1>
