<template>
  <v-app>
    <AppNavigation />    
    <v-content>
      <router-view></router-view>
    </v-content>
  </v-app>
</template>

<script>
import AppNavigation from './components/AppNavigation';
export default {
  name: 'App',
  components: {
    AppNavigation
  },
  data() {
    return {
     
    };
  }
};
</script>
