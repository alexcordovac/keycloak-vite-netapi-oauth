<template>
    <v-container>
        <v-row class="mb-2">
            <v-col>
                <WeatherAdd v-model:isOpen="isCreateModalOpen" @close="load()" />
                <v-btn @click="load" :loading="isLoading"> <v-icon size="25">mdi-reload</v-icon>Reload</v-btn>
            </v-col>
        </v-row>
        <h2 class="text-start my-4">Weather Items</h2>
        <v-row>
            <v-col cols="12" sm="6" md="4" lg="auto" v-for="item in weather" :key="item.id">
                <WeatherItem :item="item" @deleted="load()" />
            </v-col>
        </v-row>
    </v-container>
</template>

<script setup>
import { onMounted, ref } from "vue";
import api from "../http/api";
import WeatherItem from "./WeatherItem.vue";
import WeatherAdd from "./WeatherAdd.vue";

const isCreateModalOpen = ref(false)
const weather = ref([]);
const isLoading = ref(false);

const load = async () => {
    isLoading.value = true;

    try {
        const res = await api.get("/weather");
        weather.value = res.data;
    } catch (error) {
        console.error("Error loading weather forecast:", error);
        messages.add({ text: "Error loading weather forecast", color: 'error' })
    } finally {
        isLoading.value = false;
    }
};

onMounted(() => {
    load();
});

</script>
