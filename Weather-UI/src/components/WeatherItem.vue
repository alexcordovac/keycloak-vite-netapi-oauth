<template>
    <v-card class="mx-auto w-100" max-width="400" min-width="250">
        <v-card-item title="Florida">
            <template v-slot:subtitle>
                <v-icon class="me-1 pb-1" color="error" icon="mdi-alert" size="18"></v-icon>{{ props.item.summary }}
            </template>
        </v-card-item>

        <v-card-text class="py-0">
            <v-row align="center" no-gutters>
                <v-col class="text-h2">
                    {{ props.item.temperatureF }}&deg;F
                </v-col>

                <v-col class="text-right">
                    <v-icon color="error" icon="mdi-weather-hurricane" size="60"></v-icon>
                </v-col>
            </v-row>
        </v-card-text>

        <div class="d-flex py-3 justify-space-between" no-gutters>
            <v-list-item density="compact" prepend-icon="mdi-weather-windy">
                <v-list-item-subtitle>{{ props.item.date }}</v-list-item-subtitle>
            </v-list-item>

            <v-list-item density="compact" prepend-icon="mdi-weather-pouring">
                <v-list-item-subtitle>48%</v-list-item-subtitle>
            </v-list-item>
        </div>

        <v-divider></v-divider>

        <v-card-actions>
            <WeatherDelete :id="props.item.id" @itemDeleted="emit('deleted')" />
        </v-card-actions>

    </v-card>
</template>
<script setup>
import { ref } from 'vue';
import WeatherDelete from './WeatherDelete.vue';

const props = defineProps(['item']);
const emit = defineEmits(['deleted']);

</script>


<style>
.v-list-item__prepend>.v-badge~.v-list-item__spacer,
.v-list-item__prepend>.v-icon~.v-list-item__spacer,
.v-list-item__prepend>.v-tooltip~.v-list-item__spacer {
    width: 15px !important;
}
</style>
