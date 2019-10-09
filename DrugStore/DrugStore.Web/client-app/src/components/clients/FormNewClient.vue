<template>
  <v-card>
    <v-card-title>
      <span class="headline">Nuevo Cliente</span>
    </v-card-title>
    <v-card-text>
      <v-container>
        <v-row>
          <v-col cols="12" sm="6" md="12">
            <v-text-field label="Nombres*" v-model="name" required></v-text-field>
          </v-col>         
          
          <v-col cols="12" md="12">
            <v-text-field label="Apellidos*" v-model="lastName" required></v-text-field>
          </v-col>          

          <v-col cols="12">
            <v-text-field label="Tipo de documento" v-model="documentType"></v-text-field>
          </v-col>                    

            <v-col cols="12">
            <v-text-field label="Numero de documento" v-model="documentNumber" ></v-text-field>
          </v-col>

          <v-col cols="12">
            <v-text-field label="Numero de telefono" v-model="phoneNumber"></v-text-field>
          </v-col>
          <v-col cols="12">
            
            <v-select
              v-model="estado"
              :items="[{'estado':'Activo', 'value':true}, {'estado':'Inactivo', 'value':false}]"   
              item-text="estado"
              item-value="value"                       
              label="Estado*"
              required
            ></v-select>  
          </v-col>
        </v-row>
      </v-container>
      <small>*indica campo obligatorio</small>
      
    </v-card-text>
    <v-card-actions>
      <div class="flex-grow-1"></div>
      <v-btn color="blue darken-1" text @click="cancel()">Cerrar</v-btn>
      <v-btn color="blue darken-1" text @click="save()">Guardar</v-btn>
    </v-card-actions>
  </v-card> 
</template>

<script>
  export default {    
    data() {
      return {
        name:'',     
        lastName:'',
        documentType:'',
        documentNumber:'',
        phoneNumber:'',        
        estado:'',       
        menu1: false
      }
    },
    methods: {
      save() {
        this.$emit('save',{           
          'name':this.name,
          'lastName':this.lastName,
          'documentType':this.documentType,    
          'documentNumber':this.documentNumber,                    
          'phoneNumber':this.phoneNumber,          
          'condition':this.estado
        })
      },
      cancel(){
        this.$emit('cancel',false)
      }
    },
  }
</script>

