<template>
  <v-container>

    <div class="d-flex justify-center">
      <User />
    </div>

    <section>
      <WeatherList />
    </section>

  </v-container>
</template>

<script setup>
import WeatherList from "../components/WeatherList.vue";
import User from "@/components/User.vue";

</script>
